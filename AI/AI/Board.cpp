#include "stdafx.h"

vector<vector<int>> Board::CELL_WEIGHTS = {
{4, -3, 2, 2, 2, 2, -3, 4},
{-3, -4, -1, -1, -1, -1, -4, -3 },
{2, -1, 1, 0, 0, 1, -1, 2},
{2, -1, 0, 1, 1, 0, -1, 2 },
{2, -1, 0, 1, 1, 0, -1, 2 },
{2, -1, 1, 0, 0, 1, -1, 2},
{-3, -4, -1, -1, -1, -1, -4, -3 },
{4, -3, 2, 2, 2, 2, -3, 4},
};

const float Board::STATIC_MULT = 1.0f;
const float Board::MOBILITY_MULT = 1.0f;
const float Board::COIN_MULT = 1.0f; 
const  float Board::CORNER_MULT = 1.0f;

string getCellStr(CellState c){
    switch(c){
        case EMPTY:
            return ".";
        case BLACK:
            return "B";
        case WHITE:
            return "W";
    }
    return "?";
}

CellState getCellState(char c) {
    switch (c) {
    case '.':
        return EMPTY;
    case 'B':
        return BLACK;
    case 'W':
        return WHITE;
    }
    return EMPTY;
}

CellState otherColor(CellState color){
    return color == BLACK ? WHITE : BLACK;
}

Board::Board(){
    reset();
}

Board::Board(const string& snapshot) {
    for (int i = 0, k = 0; i < rows.size(); i++) {
        for (int j = 0; j < rows[i].size(); j++, k++) {
            rows[i][j] = getCellState(snapshot[k]);
        }
    }
}
    
void Board::reset(){
    for(auto& row : rows){
        for(auto& c : row){
            c = EMPTY;
        }
    }

    rows[3][3] = WHITE; rows[3][4] = BLACK;
    rows[4][3] = BLACK; rows[4][4] = WHITE;
}

int di[] = {1, 0,-1, 0, 1, 1,-1,-1};
int dj[] = {0, 1, 0,-1, 1,-1, 1,-1};
bool Board::isMoveValid(int i, int j, CellState myColor) const {
    if(rows[i][j] != EMPTY){
        return false;
    }
    for(int k = 0; k < 8; k++){
        if(canCapture(i, j, di[k], dj[k], myColor)){
            return true;
        }
    }
    return false;
}

vector<pair<int,int>> Board::getValidMoves(CellState myColor) const{
    vector<pair<int,int>> moves;
    for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
            if(isMoveValid(i, j, myColor)){
                moves.emplace_back(i, j);
            }
        }
    }
    return moves;
}

void Board::makeMove(int i, int j, CellState myColor){
    rows[i][j] = myColor;
    for(int k = 0; k < 8; k++){
        if(canCapture(i, j, di[k], dj[k], myColor)){
            capture(i, j, di[k], dj[k], myColor);
        }
    }
}

bool Board::checkRange(int coord) const{
    return 0 <= coord && coord < 8; 
}

bool Board::canCapture(int i, int j, int di, int dj, CellState myColor) const{
    i += di;
    j += dj;
    CellState enemyColor = otherColor(myColor);
    bool enemyPieceFound = false;
    while(checkRange(i) && checkRange(j) && rows[i][j] != EMPTY){
        if(rows[i][j] == enemyColor){
            enemyPieceFound = true;
        }
        else{
            return enemyPieceFound;
        }
        i += di;
        j += dj;
    }
    return false;
}

void Board::capture(int i, int j, int di, int dj, CellState myColor){
    i += di;
    j += dj;
    while(rows[i][j] != myColor){
        rows[i][j] = myColor;
        i += di;
        j += dj;
    }
}

float Board::calcScore(CellState myColor) const{
    CellState enemyColor = otherColor(myColor);
    return STATIC_MULT * calcStaticScore(myColor, enemyColor)
        + MOBILITY_MULT * calcMobilityScore(myColor, enemyColor)
        + COIN_MULT * calcCoinScore(myColor, enemyColor)
        + CORNER_MULT * calcCornerScore(myColor, enemyColor);
}

float Board::calcStaticScore(CellState myColor, CellState enemyColor) const{
    float score = 0;
    for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
            int mult = 0;
            if(rows[i][j] == myColor){
                mult = 1;
            }
            if(rows[i][j] == enemyColor){
                mult = -1;
            }
            score += mult * CELL_WEIGHTS[i][j];
        }
    }
    return score;
}

float Board::calcMobilityScore(CellState myColor, CellState enemyColor) const{
    auto myMoves = getValidMoves(myColor).size();
    auto enemyMoves = getValidMoves(enemyColor).size();
    if(myMoves + enemyMoves == 0){
        return 0;
    }
    return 100.0f * (myMoves - enemyMoves) / (myMoves + enemyMoves);
}

float Board::calcCoinScore(CellState myColor, CellState enemyColor) const{
    int myCoins = 0, enemyCoins = 0;
    for(auto& row : rows){
        for(auto& c : row){
            if(c == myColor){
                myCoins++;
            }
            if(c == enemyColor){
                enemyCoins++;
            }
        }
    }
    return 100.0f * (myCoins - enemyCoins) / (myCoins + enemyCoins);
}

int cornerI[] = {0, 7, 0, 7};
int corenrJ[] = {0, 0, 7, 7}; 
float Board::calcCornerScore(CellState myColor, CellState enemyColor) const{
    int myCorners = 0, enemyCorners = 0;
    for(int k = 0; k < 4; k++){
        CellState cornerColor = rows[cornerI[k]][corenrJ[k]];
        myCorners += cornerColor == myColor;
        enemyCorners += cornerColor == enemyColor;
    }
    if(myCorners + enemyCorners == 0){
        return 0;
    }
    return 100.0f * (myCorners - enemyCorners) / (myCorners + enemyCorners);
}

string Board::toString() const {
    string ans = "";
    for (auto& row : rows) {
        for (auto& c : row) {
            ans += getCellStr(c);
        }
    }
    return ans;
}

ostream& operator<<(ostream& out, const Board& board){
    auto separator = [&](){
        for(int i = 0; i < 17; i++){
            out << "-";
        }
        out << "\n";
    };

    separator();
    for(auto& row : board.rows){
        out << "|";
        for(auto& c : row){
            out << getCellStr(c) << "|";
        }
        out << "\n";
        separator();
    }

    return out;
}