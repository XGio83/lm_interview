# Toy Robot

ToyRobot implementation for the interview :)

## Getting Started

The solution contains 3 projects:

```
GC.Toyrobot.............Toy robot library
GC.ToyRobot.Console.....Console application to interact with toy robot
GC.Toyrobot.Test........Test project
```

### Console

You can provide commands to Toy Robot one by one..

```
ToyRobox.exe -t 5 -s 1
PLACE 0,0,NORTH
MOVE
...
```

or read them from a text file

```
ToyRobox.exe -t 5 -s 1 -f "x:\data.txt"
```

#### Parameters

```
-f, --file     File with text commands

-t, --table    Required. table size

-s, --speed    Required. robot speed

--help         Display help screen.

--version      Display version information.
```
