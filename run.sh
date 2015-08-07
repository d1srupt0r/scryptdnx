#!/bin/bash

# BUILD
xbuild /nologo /verbosity:quiet

# LOG
gendarme --quiet --log todo.log --html bin/gendarme.html  bin/scryptdnx.exe

# RUN
mono bin/scryptdnx.exe $@
