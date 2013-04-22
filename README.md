permutation-generator-csharp
============================

Permutations generator written in C#. Allows to generate permutations of the data stored in CSV files.

Download binaries: http://sdrv.ms/XZ922m

This application has command line and windows forms interfaces.

It allows to generate permutations for data stored in comma-separated format (CSV). For example for file with contents:
(first row is a header)

market,language

en-US,English

uk-UA,Ukrainian


Apllication will generate Cartesian product: 

en-US,English

en-US,Ukrainian

uk-UA,English

uk-UA,Ukrainian

To launch application in Windows Forms mode simply enter application name: 

PermutationsGenerator.exe

See screenshot here: https://vyntza.bn1.livefilestore.com/y1pYgbL6fWd_xCUmFXbruhe-KjREkEdX3q8cjISfVL4ST3p_naKFzAfm95kluO-fqVb63llKAw0jBNFFmfACr2io9rPIoWsnEJk/PermutationsGeneratorUI.png?psid=1

Usage of command line interface (use /? or -help to see this):

Usage: PermutationsGenerator.exe <inputCsvFile> -print|<resultFileName> [<delimiter>]

Samples:

Print permutation to screen:

  PermutationsGenerator.exe c:\temp\input.scv -print

Save permutation to file:

  PermutationsGenerator.exe c:\temp\input.scv c:\temp\output.csv

Use not standard delimiter (comma used by default):

  PermutationsGenerator.exe c:\temp\input.scv c:\temp\output.csv ;
