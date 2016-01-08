#!/bin/bash

# BUILD
xbuild /nologo #/verbosity:quiet

# LOG
gendarme --quiet --html bin/gendarme.html bin/scryptdnx.exe
grep -rn "ToDo" --include="*.cs" > todo.log
