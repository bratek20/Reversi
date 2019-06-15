#ifndef COMMUNICATOR_H
#define COMMUNICATOR_H

#include <string>

class Communicator {
public:
    static std::string read(const std::string& msg);
};

#endif
