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
    searchResult = urllib.request.urlopen(completeURL).read()
    return searchResult

def ParseResultString(resultString):
    ResultObject = json.loads(resultString)
    result = VocDef()
    result.Term = ResultObject[0]['meta']['app-shortdef']['hw']
    result.FuncLabel = ResultObject[0]['meta']['app-shortdef']['fl']
    result.ShortDef = ResultObject[0]['meta']['app-shortdef']['def']
    return result

if __name__ == "__main__":
    if (len(sys.argv) <= 1):
        fileToRead = input('Enter the file contains vocabularies: ')
        # Handle the input.
    else:
        for file in sys.argv:
            if (file != sys.argv[0]):
                # open file
                try:
                    ReadFiles.append(open(file, "r"))
                except:
                    print(file + ' file does not exist.')
                    exit()
                else:
                    print('Read file successfully. Creating file to store searching results...')
                # create file
                try:
                    WriteFiles.append(open('DefAdded_' + file, "w"))
                except: 
                    print("Couldn't create file.")
                    exit()
                else:
                    print('Create file successfully. Working on searching...')
    # voc = input('Enter what you want to look up: ')
    # defString = getDef(voc)
    # DefObj = ParseResultString(defString)
    # print(DefObj.Term)
    # print(DefObj.FuncLabel)
    # for shdef in DefObj.ShortDef:
    #     print(shdef)