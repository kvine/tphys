#include "MyMutex.h"

MyMutex::MyMutex()
{	
}

MyMutex::~MyMutex()
{	
}

void MyMutex::Lock()
{	
	m_mutex.lock();
}

void MyMutex::Unlock()
{	
	m_mutex.unlock();
}