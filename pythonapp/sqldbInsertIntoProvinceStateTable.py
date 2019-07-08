#!/usr/bin/python -tt


import pyodbc
import sys
import os
import re #RegEx library

class SaveUS_States:

  def __init__(self, inFile = '', dbTable = ''):
##    self.dbConnection = pyodbc.connect(r"DRIVER={SQL Server};SERVER=lottotry.com;DATABASE=gosoftso_mssql;UID=gosoftso_henry;PWD=Hma@1985",
##                                       autocommit=True)
    self.dbConnection = pyodbc.connect(r"DRIVER={SQL Server};SERVER=lottotry.com;DATABASE=gosoftso_mssql;UID=sa;PWD=Hma@1985",
                                       autocommit=True)

    self.inFile = inFile
    self.dbTable = dbTable
    self.cursor = self.dbConnection.cursor()

  def removeMultipleSpaces(self):
    infile = open(self.inFile, "r")
    outfile = open("tmp.txt", "w")
    for line in infile.readlines():
      line = re.sub(r'(\s+)', ' ', line)
      outfile.write(line + "\n")

    infile.close()
    outfile.close()
    
    
  def removeColumn(self, destFile):
    infile = open(self.inFile, "r")
    outfile = open(destFile, "w")
    
    for line in infile.readlines():
      match = re.search(r'([\w+\s\w+]*)\s*(\s\w+\s)\s*(\s[\w*\.]*\s)\s*(\d+)\s*', line)
      if match:
        s1 = match.group(1)
        s2 = match.group(2)
        s3 = match.group(4)
        outfile.write(s1 + s2 + str(s3) + "\n")

    infile.close()
    outfile.close()
    
    

  def dbSelectAll(self):
    self.cursor.execute("select * from " + self.dbTable)
    rows = self.cursor.fetchall()
    for row in rows:
      print row

  def dbInsert(self):
    infile = open(self.inFile, 'r')
    for line in infile.readlines():
      self.cursor.execute("insert into " + self.dbTable + " VALUES('%s')" % line)

    infile.close()

  def dbClose(self):
    self.dbConnection.close()


def main():
    obj = SaveUS_States('Canada_US_Provinces_States.txt', 'tblProvinceState')
    obj.dbInsert()
    obj.dbSelectAll()
    


if __name__ == '__main__':
  main()

