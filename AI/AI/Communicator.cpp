#include "stdafx.h"

string Communicator::read(const string& msg) {
    vector<string> info;
    info.push_back("");
    for (char c : msg) {
        if (c == '#') {
            info.push_back("");
            continue;
        }
        info.back() += c;
    }

    string boardSnapshot = info[0];
    string myColorStr = info[1];
    string recurenceDeepStr = info[2];

    Board board(boardSnapshot);
    CellState myColor = getCellState(myColorStr[0]);
    int recurenceDeep = stoi(recurenceDeepStr);

    SolverMove ans = Solver::solve(board, myColor, recurenceDeep);
    return to_string(ans.i) + "#" + to_string(ans.j) + "#" + board.toString();
}

