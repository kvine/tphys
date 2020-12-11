#include <string.h>
#include "hello.h"
int add(int a,int b){
  return a + b + 1;
}
void hello(char* str,char* res){
	strcat(str,"world.");
	strcpy(res,str);
}