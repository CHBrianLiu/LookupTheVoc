# Import libraries
import sys
import json
import urllib.request

# Import modules 
import credentials

# Variables
requestURL = 'https://dictionaryapi.com/api/v3/references/learners/json/'
myKey = credentials.credentials.key

# Files List
ReadFiles = []
WriteFiles = []

# Classes definition
class VocDef:
    Term = ''
    FuncLabel = ''
    ShortDef = []
    Example = []

# Func retrieve vocabularies' definition
def getDef(vocToSearch):
    completeURL = requestURL + vocToSearch + '?key=' + myKey
    try:
        searchResult = urllib.request.urlopen(completeURL).read()
    except:
        return -1
    else:
        return ParseResultString(searchResult)

def ParseResultString(resultString):
    ResultObject = json.loads(resultString)
    result = VocDef()
    try:
        result.Term = ResultObject[0]['meta']['app-shortdef']['hw']
        result.FuncLabel = ResultObject[0]['meta']['app-shortdef']['fl']
        result.ShortDef = ResultObject[0]['meta']['app-shortdef']['def']
    except:
        return -1
    else:
        return result

def OpenReadWriteFiles(file):
    # open file
    try:
        ReadFiles.append(open(file, "r"))
    except:
        print(file + ' file does not exist.')
        CloseOpenedFiles()
        exit()
    else:
        print('Read file successfully. Creating file to store searching results...')
    # create file
    try:
        WriteFiles.append(open('DefAdded_' + file, "w"))
    except:
        print("Couldn't create file.")
        CloseOpenedFiles()
        exit()
    else:
        print('Create file successfully. Working on searching...')
    # Finish File checking

def CloseOpenedFiles():
    for ReadFile in ReadFiles:
        ReadFile.close()
    for WriteFile in WriteFiles:
        WriteFile.close()

if __name__ == "__main__":
    # No argument provided. Prompt user to enter one.
    if (len(sys.argv) <= 1):
        fileToRead = input('Enter the file contains vocabularies: ')
        OpenReadWriteFiles(fileToRead)
    else:
        for file in sys.argv[1:]:
            OpenReadWriteFiles(file)
    # Processing reading and searching
    for i in range(0, len(ReadFiles)):
        for line in ReadFiles[i]:
            DefItem = getDef(line.rstrip())
            if (DefItem != -1):
                try:
                    LineToWrite = DefItem.Term + "\t" + DefItem.FuncLabel + "\n"
                    for idx, ShDef in enumerate(DefItem.ShortDef):
                        LineToWrite += "def " + str(idx+1) + ": " + ShDef + "\n"
                    LineToWrite += ';' 
                except: 
                    print('Concatenation error.')
                    continue
                else:
                    WriteFiles[i].write(LineToWrite + "\n")
            else:
                print(line.rstrip() + ' not find.')
    # Close all files
    CloseOpenedFiles()
    
