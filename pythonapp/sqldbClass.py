#!/usr/bin/python -tt


import pyodbc
import sys
import os
import re #RegEx library


from urllib import urlopen


class LottoDataInput:

  def __init__(self, tbl = ''):
    self.dbConnection = pyodbc.connect(r"DRIVER={SQL Server};SERVER=WEBSERVER-PC;DATABASE=gosoftso_mssql;UID=sa;PWD=Hma@1985",
                                       autocommit=True)

    self.dbTable = tbl
    self.cursor = self.dbConnection.cursor()


  def dbSelectAll(self):
    self.cursor.execute("select * from " + self.dbTable)
    rows = self.cursor.fetchall()
    for row in rows:
      print row

  # Call stored procedures in mssql
  #
  def dbLastDrawNumber(self):
    dic = { "lottery":0, "lottomax":1}
    self.cursor.execute("{call GetLastRow(?)}", (dic[self.dbTable]))
    drawnum = self.cursor.fetchone()

    return drawnum[0]

  # Call stored procedures in mssql
  #
  def dbLastDrawDate(self):
    dic = { "lottery":0, "lottomax":1}
    self.cursor.execute("{call GetLastDrawDate(?)}", (dic[self.dbTable]))
    drawdate = self.cursor.fetchone()

    #print drawdate[0], 
    return drawdate[0]


  def searchDrawNumbers(self):
    if self.dbTable == 'lottery':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/Lotto649.asp").read()
      match = re.search(r'(<span class=\"draw-info\">\s*)([\d\s]+)(\r\n\D+)(\d+)', file)
    else:
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/LOTTOMAX.asp").read()
      match = re.search(r'(<span class=\"draw-info\">\s*)([\d\s]+)(\D+)(\d+)', file)


    numbers = []
    if match:
      m1 = match.group(2)
      m1.split('\s')
      m2 = match.group(4)
      st = m1 + m2
      numbers = st.split(' ')
    return numbers

  def convertDate(self):

    dic = {"January":"01", "Feburary":"02", "March":"03", "April":"04", "May":"05",
         "June":"06", "July":"07", "August":"08", "Septempber":"09", "October":"10",
         "November":"11", "December":"12" }
    dat = self.searchDrawDate()

    list = []
    da = dat.split(' ')
    for d in da:
      match = re.search(r'([\w\d]+)(\,)', d) # remove ','
      if match:
        d = match.group(1)

      list.append(d)

    dat =  str(list[2]) + '-' + dic[list[0]] + '-' + str(list[1])
    return dat


  def searchDrawDate(self):
    m1 = ''
    m2 = ''
    m= ''
    numbers = []

    if self.dbTable == 'lottery':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/Lotto649.asp").read()

      match1 = re.search(r'(<b>Wednesday,\s*)(.+)', file)
      match2 = re.search(r'(<b>Saturday,\s*)(.+)', file)
      if match1:
        m1 = match1.group(2)
      if match2:
        m1 = match2.group(2)

    else:
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/LOTTOMAX.asp").read()
      match = re.search(r'(<b>Friday,\s*)(.+)', file)
      if match:
        m1 = match.group(2)

    m2 = re.search(r'(.+)(</b>.+)', m1)
    if m2:
      m = m2.group(1)

    return m


  def dbInsert(self):

    dn = self.dbLastDrawNumber()
    ddate = self.convertDate()
    lastdate = self.dbLastDrawDate()

    if ddate == lastdate:
      return

    dn += 1
    if self.dbTable == 'lottery':
      (n1, n2, n3, n4, n5, n6, b) = self.searchDrawNumbers()
      self.cursor.execute( "insert into " + self.dbTable + " VALUES(%d, '%s', %d, %d, %d, %d, %d, %d, %d)" % (int(dn), ddate, int(n1), int(n2), int(n3), int(n4), int(n5), int(n6), int(b)))

    elif self.dbTable == 'lottomax':
      (n1, n2, n3, n4, n5, n6, n7, b) = self.searchDrawNumbers()
      self.cursor.execute("insert into " + self.dbTable + " VALUES(%d, '%s', %d, %d, %d, %d, %d, %d, %d, %d)" % (int(dn), ddate, int(n1), int(n2), int(n3), int(n4), int(n5), int(n6), int(n7), int(b)))

    else:
      print 'Unknown database table'
      return

  def dbClose(self):
    self.dbConnection.close()

def main():
  if len(sys.argv) > 1:
    tblName = sys.argv[1]
  else:
    tblName = 'lottery'
  lottoDataInput = LottoDataInput(tblName)

  lottoDataInput.dbInsert()
  lottoDataInput.dbSelectAll()
  lottoDataInput.dbClose()
  

if __name__ == '__main__':
  main()

  
