#ifndef MyMutex_h
#define MyMutex_h
#include <mutex>

class MyMutex 
{
public:
	MyMutex();
	~MyMutex();

	void Lock();
	void Unlock();
private:
	std::mutex m_mutex;
};

#endif