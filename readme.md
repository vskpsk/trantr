# TRANTR


Trantr (v2.1.X) is a simple mathematical programming language with an 8x8 memory grid
## Installation

Install Trantr for Linux
```bash
git clone https://github.com/vskpsk/trantr.git
```

```bash
cd trantr
```

```bash
sudo ./install/install.sh
```

## Usage

Trantr utilizes a zero-based 8x8 array

### List of all commands

```trantr
GET X Y
```
Prints value of node

```trantr
SET X Y value
```
Sets value of node
```trantr
ADD X Y value
```
Add value to node
```trantr
SUB X Y value
```
Subtract value from node
```trantr
MUL X Y
```
Multiplies node by value
```trantr
DIV X Y value
```
Divides node by value
```trantr
JUMP line count
```
A loop that repeats a specified number of times and jumps to the designated line.


### references
Instead of a number like X, Y, or value, etc., So let's say that the coordinates 3, 5 have a value of 2; we can use these coordinates as a value. example:


```trantr
GET 2 1
```
This is equivalent to the following in context:
```trantr
GET [3 5] 1
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
