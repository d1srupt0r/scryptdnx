#!/bin/bash

# LOG
gendarme --quiet --log todo.log --html bin/gendarme.html  bin/scryptdnx.exe

# Execute
mono bin/scryptdnx.exe $@
