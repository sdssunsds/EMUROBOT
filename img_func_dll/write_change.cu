#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <cuda.h>
#include <device_functions.h>
#include <opencv2\opencv.hpp>
#include <iostream>
using namespace std;
__global__ void matSum(uchar* dataIn, int* dataOutSum, int* dataOutMax, int* dataOutMin, int imgHeight, int imgWidth)
{
    //__shared__ int _data[1600];
    const int number = 2048;

    extern __shared__ int _sum[];  //小图像块中求和共享数组
    __shared__ int _max[number];  //小图像块中求最大值共享数组
    __shared__ int _min[number];  //小图像块中求最小值共享数组

    int thread = threadIdx.x + threadIdx.y * blockDim.x; //一个block中所有thread的索引值
    int threadIndex = threadIdx.x + threadIdx.y * imgWidth; //每个小块中存放数据的thread索引值
    //每个小块中存放数据的block索引值
    int blockIndex1 = blockIdx.x * blockDim.x + 2 * blockIdx.y * blockDim.y * imgWidth; //40*20的上半block索引值
    int blockIndex2 = blockIdx.x * blockDim.x + (2 * blockIdx.y + 1) * blockDim.y * imgWidth; //40*20的下半block索引值

    int index1 = threadIndex + blockIndex1; //每个block中上半部分索引值
    int index2 = threadIndex + blockIndex2; //每个block中下半部分索引值

    //将待计算的40*40小图像块中的所有像素值分两次传送到共享数组中
    _sum[thread] = dataIn[index1]; //将上半部分的40*20中所有数据赋值到共享数组中
    _sum[thread + blockDim.x * blockDim.y] = dataIn[index2]; //将下半部分的40*20中所有数据赋值到共享数组中

    _max[thread] = dataIn[index1];
    _max[thread + blockDim.x * blockDim.y] = dataIn[index2];

    _min[thread] = dataIn[index1];
    _min[thread + blockDim.x * blockDim.y] = dataIn[index2];

    //memcpy(_sum, _data, 1600 * sizeof(int));
    //memcpy(_max, _data, 1600 * sizeof(int));
    //memcpy(_min, _data, 1600 * sizeof(int));  在GPU（Device）中用memcpy函数进行拷贝会导致显卡混乱，故不选择此种方式

    //利用归约算法求出40*40小图像块中1600个像素值中的和、最大值以及最小值
    for (unsigned int s = number / 2; s > 0; s >>= 1)
    {
        if (thread < s)
        {
            _sum[thread] += _sum[thread + s];
            if (_max[thread] < _max[thread + s]) { _max[thread] = _max[thread + s]; }
            if (_min[thread] > _min[thread + s]) { _min[thread] = _min[thread + s]; }
        }
        __syncthreads(); //所有线程同步
    }
    if (threadIndex == 0)
    {
        //将每个小块中的结果储存到输出中
        dataOutSum[blockIdx.x + blockIdx.y * gridDim.x] = _sum[0];
        dataOutMax[blockIdx.x + blockIdx.y * gridDim.x] = _max[0];
        dataOutMin[blockIdx.x + blockIdx.y * gridDim.x] = _min[0];
    }

}
