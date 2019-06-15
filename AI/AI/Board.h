#ifndef BOARD_H
#define BOARD_H

#include <array>
#include <vector>
#include <string>

using namespace std;

enum CellState {
    EMPTY = 0,
    BLACK = 1,
    WHITE = 2
};

CellState otherColor(CellState color);
string getCellStr(CellState c);
CellState getCellState(char c);

class Board {
    using RowType = array<CellState, 8>;
    const static float STATIC_MULT;
    const static float MOBILITY_MULT;
    const static float COIN_MULT; 
    const static float CORNER_MULT; 

public:
    Board();
    Board(const string& snapshot);
    
    void reset();
    
    bool isMoveValid(int i, int j, CellState myColor) const;
    vector<pair<int,int>> getValidMoves(CellState myColor) const;
    void makeMove(int i, int j, CellState myColor);
    float calcScore(CellState myColor) const;

    string toString() const;
    friend ostream& operator<<(ostream& out, const Board& board);

private:
    static vector<vector<int>> CELL_WEIGHTS;

    bool canCapture(int i, int j, int di, int dj, CellState myColor) const;
    void capture(int i, int j, int di, int dj, CellState myColor);
    bool checkRange(int coord) const;

    float calcStaticScore(CellState myColor, CellState enemyColor) const;
    float calcMobilityScore(CellState myColor, CellState enemyColor) const;
    float calcCoinScore(CellState myColor, CellState enemyColor) const;
    float calcCornerScore(CellState myColor, CellState enemyColor) const;
    array<RowType, 8> rows;
};

#endif
