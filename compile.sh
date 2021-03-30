rm SDESim.exe

csc -define:DEBUG -optimize -out:SDESim.exe *.cs Controls/*.cs Statistics/*.cs #Forms/*.cs

mono SDESim.exe