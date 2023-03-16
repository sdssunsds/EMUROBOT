//ClassFactory.h

#pragma once

#include <string>
#include <map>
typedef void* (*PTRCreateObject)(void);

//������Ķ���
class ClassFactory {
private:
	std::map<std::string, PTRCreateObject> m_classMap;
	ClassFactory() {}; //���캯��˽�л�

public:
	void* getClassByName(std::string className);
	void registClass(std::string name, PTRCreateObject method);
	static ClassFactory& getInstance();
};

//�������ʵ��

//@brief:��ȡ������ĵ���ʵ������  
ClassFactory& ClassFactory::getInstance() {
	static ClassFactory sLo_factory;
	return sLo_factory;
}

//@brief:ͨ���������ַ�����ȡ���ʵ��
void* ClassFactory::getClassByName(std::string className) {
	auto iter = m_classMap.find(className);
	if (iter == m_classMap.end())
		return NULL;
	else
		return iter->second();
}

//@brief:���������������ַ����Ͷ�Ӧ�Ĵ��������ĺ������浽map��   
void ClassFactory::registClass(std::string name, PTRCreateObject method) {
	m_classMap.insert(make_pair(name, method));
}
class RegisterAction {
public:
	RegisterAction(std::string className, PTRCreateObject ptrCreateFn) {
		ClassFactory::getInstance().registClass(className, ptrCreateFn);
	}
};