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

    extern __shared__ int _sum[];  //Сͼ�������͹�������
    __shared__ int _max[number];  //Сͼ����������ֵ��������
    __shared__ int _min[number];  //Сͼ���������Сֵ��������

    int thread = threadIdx.x + threadIdx.y * blockDim.x; //һ��block������thread������ֵ
    int threadIndex = threadIdx.x + threadIdx.y * imgWidth; //ÿ��С���д�����ݵ�thread����ֵ
    //ÿ��С���д�����ݵ�block����ֵ
    int blockIndex1 = blockIdx.x * blockDim.x + 2 * blockIdx.y * blockDim.y * imgWidth; //40*20���ϰ�block����ֵ
    int blockIndex2 = blockIdx.x * blockDim.x + (2 * blockIdx.y + 1) * blockDim.y * imgWidth; //40*20���°�block����ֵ

    int index1 = threadIndex + blockIndex1; //ÿ��block���ϰ벿������ֵ
    int index2 = threadIndex + blockIndex2; //ÿ��block���°벿������ֵ

    //���������40*40Сͼ����е���������ֵ�����δ��͵�����������
    _sum[thread] = dataIn[index1]; //���ϰ벿�ֵ�40*20���������ݸ�ֵ������������
    _sum[thread + blockDim.x * blockDim.y] = dataIn[index2]; //���°벿�ֵ�40*20���������ݸ�ֵ������������

    _max[thread] = dataIn[index1];
    _max[thread + blockDim.x * blockDim.y] = dataIn[index2];

    _min[thread] = dataIn[index1];
    _min[thread + blockDim.x * blockDim.y] = dataIn[index2];

    //memcpy(_sum, _data, 1600 * sizeof(int));
    //memcpy(_max, _data, 1600 * sizeof(int));
    //memcpy(_min, _data, 1600 * sizeof(int));  ��GPU��Device������memcpy�������п����ᵼ���Կ����ң��ʲ�ѡ����ַ�ʽ

    //���ù�Լ�㷨���40*40Сͼ�����1600������ֵ�еĺ͡����ֵ�Լ���Сֵ
    for (unsigned int s = number / 2; s > 0; s >>= 1)
    {
        if (thread < s)
        {
            _sum[thread] += _sum[thread + s];
            if (_max[thread] < _max[thread + s]) { _max[thread] = _max[thread + s]; }
            if (_min[thread] > _min[thread + s]) { _min[thread] = _min[thread + s]; }
        }
        __syncthreads(); //�����߳�ͬ��
    }
    if (threadIndex == 0)
    {
        //��ÿ��С���еĽ�����浽�����
        dataOutSum[blockIdx.x + blockIdx.y * gridDim.x] = _sum[0];
        dataOutMax[blockIdx.x + blockIdx.y * gridDim.x] = _max[0];
        dataOutMin[blockIdx.x + blockIdx.y * gridDim.x] = _min[0];
    }

}
