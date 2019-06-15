// AI.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Bridge.h"

extern "C" {
    __declspec(dllexport) void send(char* msg) {
        string converted(msg);
        string answer = Communicator::read(msg);
        for (int i = 0; i < answer.size(); i++) {
            msg[i] = answer[i];
        }
    }
}

