
#include "cuda_runtime.h"
#include <cuda.h>
#include <time.h>
#include <vector>
#include <opencv2/opencv.hpp>
#include "opencv2/highgui.hpp"
#include <iostream>
#include <math.h>
#include <chrono>

inline __device__
float __char_as_float(uchar b8)
{
    return __uint2float_rn(b8) / 127.5f - 1.f;
}

inline __device__
uchar __float2uchar(float f16)
{
    return __float2uint_rd(f16);
    // return __float2uint_rd(f16 * 255.f);
}

static inline void _safe_cuda_call(cudaError err, const char* msg, const char* file_name, const int line_number)
{
    if (err != cudaSuccess)
    {
        fprintf(stderr, "%s\n\nFile: %s\n\nLine Number: %d\n\nReason: %s\n", msg, file_name, line_number, cudaGetErrorString(err));
        std::cin.get();
        exit(EXIT_FAILURE);
    }
}

#define SAFE_CALL(call,msg) _safe_cuda_call((call),(msg),__FILE__,__LINE__)

inline __device__
int clip(int x, int a, int b)
{
    return x >= a ? (x < b ? x : b - 1) : a;
}

// enlarge the original image k times in x and y direction
// write dataOut coalesced
__global__ void inter_nearest_k(uchar3* dataIn, uchar3* dataOut, int imgHeight, int imgWidth, int imgHeight_k, int imgWidth_k, int k)
{
    int xIdx = threadIdx.x + blockIdx.x * blockDim.x;
    int yIdx = threadIdx.y + blockIdx.y * blockDim.y;

    if (xIdx < imgWidth_k && yIdx < imgHeight_k)
    {
        dataOut[yIdx * imgWidth_k + xIdx] = dataIn[(yIdx / k) * imgWidth + xIdx / k];
    }
}

// bilinear interpolation, enlarge k times in x and y direction
// 浮点型乘法
__global__ void inter_liner_k(uchar3* dataIn, uchar3* dataOut, int imgHeight, int imgWidth, int imgHeight_k, int imgWidth_k, float scale)
{
    // __shared__ float shared_eles[34][34];
    int xIdx = threadIdx.x + blockIdx.x * blockDim.x;
    int yIdx = threadIdx.y + blockIdx.y * blockDim.y;

    if (xIdx < imgWidth_k && yIdx < imgHeight_k)
    {
        float fx = (float)((xIdx + 0.5f) * scale - 0.5f);
        int sx = floorf(fx);
        fx -= sx;
        sx = min(sx, imgWidth - 1);
        int sx2 = min(sx + 1, imgWidth - 1);
        if (sx < 0)
            sx2 = 0, sx = 0;

        float2 cbufx;
        cbufx.x = 1.f - fx;
        cbufx.y = fx;

        float fy = (float)((yIdx + 0.5f) * scale - 0.5f);
        int sy = floorf(fy);
        fy -= sy;
        sy = min(sy, imgHeight - 1);
        int sy2 = min(sy + 1, imgHeight - 1);
        if (sy < 0)
            sy2 = 0, sy = 0;

        float2 cbufy;
        cbufy.x = 1.f - fy;
        cbufy.y = fy;

        // if(sx % 2 == 0 || sy % 2 == 0)
        // uchar3 s11 = make_uchar3(0,0,0), s12 = make_uchar3(0,0,0), s21 = make_uchar3(0,0,0), s22 = make_uchar3(0,0,0);
        // 从global memory加载数据花费1500 us
        uchar3 s11 = dataIn[sy * imgWidth + sx];
        uchar3 s12 = dataIn[sy * imgWidth + sx2];
        uchar3 s21 = dataIn[sy2 * imgWidth + sx];
        uchar3 s22 = dataIn[sy2 * imgWidth + sx2];
        // __syncthreads();

        float h_rst00x, h_rst01x, h_rst00y, h_rst01y, h_rst00z, h_rst01z;
        h_rst00x = s11.x * cbufx.x + s12.x * cbufx.y;
        h_rst01x = s21.x * cbufx.x + s22.x * cbufx.y;
        h_rst00y = s11.y * cbufx.x + s12.y * cbufx.y;
        h_rst01y = s21.y * cbufx.x + s22.y * cbufx.y;
        h_rst00z = s11.z * cbufx.x + s12.z * cbufx.y;
        h_rst01z = s21.z * cbufx.x + s22.z * cbufx.y;

        // 写入global memory花费1500 us， 所有其他的运算花费1000 us
        dataOut[yIdx * imgWidth_k + xIdx].x = h_rst00x * cbufy.x + h_rst01x * cbufy.y; // B
        dataOut[yIdx * imgWidth_k + xIdx].y = h_rst00y * cbufy.x + h_rst01y * cbufy.y; // G
        dataOut[yIdx * imgWidth_k + xIdx].z = h_rst00z * cbufy.x + h_rst01z * cbufy.y; // R
    }
}

int resize(void)
{
    int k = 11;
    float scale = 1.f / (float)k;
    cv::Mat img_ori = cv::imread("lisfan-70.jpg");
    int imgWidth = img_ori.cols;
    int imgHeight = img_ori.rows;
    int imgHeight_k = imgHeight * k;
    int imgWidth_k = imgWidth * k;

    float runtime;
    

    uchar3* d_in;
    uchar3* d_out;

    cv::Mat img_resize_gpu(imgHeight_k, imgWidth_k, CV_8UC3);

    SAFE_CALL(cudaMalloc((void**)&d_in, imgHeight * imgWidth * sizeof(uchar3)), "cudaMalloc d_in fialed");
    SAFE_CALL(cudaMalloc((void**)&d_out, imgHeight_k * imgWidth_k * sizeof(uchar3)), "cudaMalloc d_out failed");

    SAFE_CALL(cudaMemcpy(d_in, img_ori.data, imgHeight * imgWidth * sizeof(uchar3), cudaMemcpyHostToDevice), "d_in cudaMemcpyHostToDevice failed");

    dim3 threadsPerBlock(32, 32);
    dim3 blocksPerGrid((imgWidth_k + threadsPerBlock.x - 1) / threadsPerBlock.x, (imgHeight_k + threadsPerBlock.y - 1) / threadsPerBlock.y);

    inter_liner_k << <blocksPerGrid, threadsPerBlock >> > (d_in, d_out, imgHeight, imgWidth, imgHeight_k, imgWidth_k, scale);
    cudaEventDestroy(start);
    cudaEventDestroy(stop);
    // cudaDeviceSynchronize(); //CPU端计时，需要同步CPU和gpu，否则测速结果为cpu启动内核函数的速度
    // k=1, 562.528 us; k=3, 791.072 us; k=5, 1267.71 us; k=7, 1933.28 us; k=9, 2896.38 us; k=11, 3989.31 us
    // 不写入global memory，resize完了之后直接用
    // k=1, 525.568 us; k=3, 590.449 us; k=5, 754.656 us; k=7, 768.704 us; k=9, 985.76 us; k=11, 1086.08 us
    std::cout << "cudaEvent_t time: " << runtime * 1000 << " us" << std::endl;

    SAFE_CALL(cudaMemcpy(img_resize_gpu.data, d_out, imgHeight_k * imgWidth_k * sizeof(uchar3), cudaMemcpyDeviceToHost), "d_out cudaMemcpyDeviceToHost failed");

    cv::Mat resid(imgHeight_k, imgWidth_k, CV_8UC3);
    int diff = 0;
    for (int j = 0; j < imgHeight_k; j++)
    {
        for (int i = 0; i < imgWidth_k; i++)
        {
            resid.at<cv::Vec3b>(j, i)[0] = 100 * (img_resize_cpu.at<cv::Vec3b>(j, i)[0] - img_resize_gpu.at<cv::Vec3b>(j, i)[0]);
            resid.at<cv::Vec3b>(j, i)[1] = 100 * (img_resize_cpu.at<cv::Vec3b>(j, i)[1] - img_resize_gpu.at<cv::Vec3b>(j, i)[1]);
            resid.at<cv::Vec3b>(j, i)[2] = 100 * (img_resize_cpu.at<cv::Vec3b>(j, i)[2] - img_resize_gpu.at<cv::Vec3b>(j, i)[2]);
            if (abs(img_resize_cpu.at<cv::Vec3b>(j, i)[0] - img_resize_gpu.at<cv::Vec3b>(j, i)[0]) > 0 ||
                abs(img_resize_cpu.at<cv::Vec3b>(j, i)[1] - img_resize_gpu.at<cv::Vec3b>(j, i)[1]) > 0 ||
                abs(img_resize_cpu.at<cv::Vec3b>(j, i)[2] - img_resize_gpu.at<cv::Vec3b>(j, i)[2]) > 0) // > 1  diff: 0
            {
                diff += 1;
            }
        }
    }
    std::cout << "diff: " << diff << std::endl;

    SAFE_CALL(cudaFree(d_in), "free d_in failed");
    SAFE_CALL(cudaFree(d_out), "free d_out failed");

    cv::imwrite("lisfan-70_" + std::to_string(k) + "_gpu.jpg", img_resize_gpu);
    cv::imwrite("cpu_gpu_residual_" + std::to_string(k) + ".jpg", resid);

    return 0;
}
