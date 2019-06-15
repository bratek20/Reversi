#include "stdafx.h"

#include <limits>
#include <algorithm>

const float INF = numeric_limits<float>::max();
int MIN_MAX_DEEP = 0;
bool SORT_MOVES = false; 

SolverMove Solver::solve(Board board, CellState myColor, int recurrenceDeep){
    if(recurrenceDeep == 0){
        auto moves = board.getValidMoves(myColor);
        if(moves.empty()){
            return {-1, -1};
        }
        random_shuffle(moves.begin(), moves.end());
        return {moves[0].first, moves[0].second};
    }

    MIN_MAX_DEEP = recurrenceDeep;
    SORT_MOVES = MIN_MAX_DEEP >= 5;

    MinMaxData ans = minMax(board, myColor, MIN_MAX_DEEP, true, false, -INF, INF);
    return ans.pos;
}

MinMaxData Solver::minMax(Board board, CellState initColor, int depth, bool maximise, bool turnPassed, float alpha, float beta){
    if(depth == 0){
        return MinMaxData(board, initColor); 
    }

    CellState myColor = maximise ? initColor : otherColor(initColor);
    vector<PossibleMove> moves = getPossibleMoves(board, myColor, initColor);
    if(moves.empty()){
        if(turnPassed){
            return MinMaxData(board, initColor); // both sides passed
        }
        else{
            MinMaxData passMove = minMax(board, initColor, depth - 1, !maximise, true, alpha, beta);
            passMove.pos = {-1, -1};
            return passMove;
        }
    }

    if(SORT_MOVES){
        sort(moves.begin(), moves.end(), [&](const PossibleMove& m1, const PossibleMove& m2){
            return compare(m1.score, m2.score, maximise);
        });
    }
    

    MinMaxData ans(maximise);
    for(auto& move : moves){
        MinMaxData cur = minMax(move.board, initColor, depth - 1, !maximise, false, alpha, beta);
        if(compare(cur.score, ans.score, maximise)){
            ans = cur;
            ans.pos = move.pos;
        }

        if(maximise){
            alpha = max(alpha, ans.score);
        }
        else{
            beta = min(beta, ans.score);
        }
        if(beta <= alpha){
            break;
        }
    } 
    return ans;
}

bool Solver::compare(float a, float b, bool maximise) {
    if(maximise){
        return a > b;
    }
    return a < b;
}

vector<PossibleMove> Solver::getPossibleMoves(Board board, CellState myColor, CellState initColor){
    vector<PossibleMove> moves;
    for(pair<int, int> pos : board.getValidMoves(myColor)){
        PossibleMove move;
        move.pos = {pos.first, pos.second};
        move.board = board;
        move.board.makeMove(move.pos.i, move.pos.j, myColor);
        if(SORT_MOVES){
            move.score = move.board.calcScore(initColor);
        }
        moves.push_back(move);
    }
    return moves;
}

MinMaxData::MinMaxData(const Board& board, CellState myColor){
    pos = {-1, -1};
    score = board.calcScore(myColor);
}

MinMaxData::MinMaxData(bool maximise){ 
    score = maximise ? -INF : INF;
}