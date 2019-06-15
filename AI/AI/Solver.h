#ifndef REVERSI_SOLVER_H
#define REVERSI_SOLVER_H

#include "Board.h"

struct SolverMove {
    int i;
    int j;
};

struct PossibleMove {
    SolverMove pos;
    Board board;
    float score;
};

struct MinMaxData {
    SolverMove pos;
    float score;

    MinMaxData(const Board& board, CellState myColor);
    MinMaxData(bool maximise);
};

class Solver {
public:
    static SolverMove solve(Board board, CellState myColor, int recurrenceDeep);

private:
    static MinMaxData minMax(Board board, CellState initColor, int depth, bool maximise, bool turnPassed, float alpha, float beta);
    static std::vector<PossibleMove> getPossibleMoves(Board board, CellState myColor, CellState initColor);
    static bool compare(float a, float b, bool maximise);
};

#endif
