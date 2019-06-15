#include "stdafx.h"

#include <iostream>

using namespace std;

int main() {
    string msg = "...........................BBB.....WB...........................#W#5";
    msg = Communicator::read(msg);
    cout << "elo";
    return 0;
}

int WinMain(
    HINSTANCE hInstance,
    HINSTANCE hPrevInstance,
    LPSTR     lpCmdLine,
    int       nShowCmd
) {
    return main();
}