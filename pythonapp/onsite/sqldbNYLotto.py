#!/usr/bin/python -tt


import pyodbc
import sys
import os
import re #RegEx library


from urllib import urlopen

class Stack:
  def __init__(self):
    self.__storage = []

  def isEmpty(self):
    return len(self.__storage) == 0

  def push(self,p):
    self.__storage.append(p)

  def pop(self):
    return self.__storage.pop()
  
  
class LottoDataInput:

  def __init__(self, tbl = ''):
    self.dbConnection = pyodbc.connect(r"DRIVER={SQL Server};SERVER=8.8.246.35;DATABASE=gosoftso_mssql;UID=gosoftso_henry;PWD=Hma@1985",
                                       autocommit=True)

    self.dbTable = tbl
    self.cursor = self.dbConnection.cursor()
    

##  def formatDataFile(self):
##    rawDataFile = open("FloridaLottery.txt", "r")
##    outDataFile = open("FloridaLotto.txt", "w")
##
##    for line in rawDataFile.readlines():
##      line = re.sub(r'(\s+)', ' ', line)
##      outDataFile.write(line + "\n")
##    
##    rawDataFile.close()
##    outDataFile.close()
##    
##  def formatDataFile2(self):
##    rawDataFile = open("FloridaLotto.txt", "r")
##    outDataFile = open("Output1.txt", "w")
##
##    for line in rawDataFile.readlines():
##      line = re.sub(r'(-)', ' ', line)
##      outDataFile.write(line + "\n")
##    
##    rawDataFile.close()
##    outDataFile.close()
##    
##  def formatDataFile3(self):
##    rawDataFile = open("Output1.txt", "r")
##    outDataFile1 = open("FinalOut1.txt", "w")
##    outDataFile2 = open("FinalOut2.txt", "w")
##
##    for line in rawDataFile.readlines():
##      match = re.search(r'(\d+.+\sX\d)\s(\d+.+\sX\d)', line)
##      if match:
##        m1 = match.group(1)
##        m2 = match.group(2)
##        outDataFile1.write(m1 + "\n")
##        outDataFile2.write(m2 + "\n")
##    
##    rawDataFile.close()
##    outDataFile1.close()
##    outDataFile2.close()
##


##  def reverseOrder(self):
##    infile = open("FloridaLotto.txt", "r")
##    outfile = open("FloridaLottoReversed.txt", "w")
##    stack = Stack()
##    
##    for line in infile.readlines():
##      stack.push(line)
##
##    i = 1
##    while not stack.isEmpty():
##      s = stack.pop()
##      s = re.sub(r'(X)', ' ', s)
##      s = str(i) + ' ' + s
##      i += 1
##      outfile.write(s)
##      
##    infile.close()
##    outfile.close()
    
  def removeMultipleSpaces(self):
    infile = open("FloridaLotto.txt", "r")
    outfile = open("tmp.txt", "w")
    for line in infile.readlines():
      line = re.sub(r'(\s+)', ' ', line)
      outfile.write(line + "\n")

    infile.close()
    outfile.close()
    
    

  def dbSelectAll(self):
    self.cursor.execute("select * from " + self.dbTable)
    rows = self.cursor.fetchall()
    for row in rows:
      print row

  # Call stored procedures in mssql
  #
  def dbLastDrawNumber(self):
    dic = { "lottery":0, "lottomax":1, "BC49":2, "FloridaLotto":3, "MegaMillions":4, "PowerBall":5, 'NYLotto':6  }
    self.cursor.execute("{call GetLastRow(?)}", (dic[self.dbTable]))
    drawnum = self.cursor.fetchone()

    return drawnum[0]

  # Call stored procedures in mssql
  #
  def dbLastDrawDate(self):
    dic = { "lottery":0, "lottomax":1, "BC49":2, "FloridaLotto":3, "MegaMillions":4, "PowerBall":5, 'NYLotto':6 }
    self.cursor.execute("{call GetLastDrawDate(?)}", (dic[self.dbTable]))
    drawdate = self.cursor.fetchone()

    #print drawdate[0], 
    return drawdate[0]


  def searchDrawNumbers(self):
    if self.dbTable == 'lottery':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/Lotto649.asp").read()
      match = re.search(r'(<span class=\"draw-info\">\s*)([\d\s]+)(118\n\D+)(\d+)', file)
    elif self.dbTable == 'lottomax':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/LOTTOMAX.asp").read()
      match = re.search(r'(<span class=\"draw-info\">\s*)([\d\s]+)(\D+)(\d+)', file)
    elif self.dbTable == 'BC49':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/Lotto649.asp").read()
      match = re.search(r'(alt=\"BC/49\".+\r\n\D+<span class=\"draw-info\">\s*)([\d\s]+)(\r\n\D+)(\d+)', file)
    elif self.dbTable == 'FloridaLotto':
      file = urlopen("http://www.flalottery.com/lottoMain.do").read()
      match = re.search(r'(\d+-\d+-\d+-\d+-\d+-\d+\s+)(X\s+)(\d)', file)
    elif self.dbTable == 'MegaMillions':
      file = urlopen("http://www.calottery.com/Games/MegaMillions/WinningNumbers/").read()
      
      match = re.search(r'(.+lblMMNum1\">)(\d*)', file)
      if match:
        n1 = match.group(2)
      match = re.search(r'(.+lblMMNum2\">)(\d*)', file)
      if match:
        n2 = match.group(2)
      match = re.search(r'(.+lblMMNum3\">)(\d*)', file)
      if match:
        n3 = match.group(2)
      match = re.search(r'(.+lblMMNum4\">)(\d*)', file)
      if match:
        n4 = match.group(2)
      match = re.search(r'(.+lblMMNum5\">)(\d*)', file)
      if match:
        n5 = match.group(2)
      match = re.search(r'(.+lblMMMega\">)(\d*)', file)
      if match:
        n6 = match.group(2)

    elif self.dbTable == 'PowerBall':
      file = urlopen("http://www.lotteryusa.com/lottery/PB/PB_fcur.html").read()
      match = re.search(r'(<span class=\'results\'>)([\d*,\s]+)(Powerball:\s)(\d*)(,\sPower Play:\s)(\d*)(<.+)', file)
      lst = []
      if match:
        m1 = match.group(2)     
        m1 = re.sub(r'(,)', '', m1)
        lst = m1.split(' ')
        n1 = lst[0]
        n2 = lst[1]
        n3 = lst[2]
        n4 = lst[3]
        n5 = lst[4]
        n6 = match.group(4)     
        n7 = match.group(6)     
      
    elif self.dbTable == 'NYLotto':
      file = urlopen("http://www.alllotto.com/latest_new_york_lottery_results.php").read()
      match = re.search(r'(<td width=\'50%\' wrap><div class=\'normal\'>\n\s+)([\d*,\s]+)(Bonus\s*)(\d*)(\s*<.+)', file)
      lst = []
      if match:
        m = match.group(2)
        m.strip()
        b = match.group(4)
        b.strip()
        m = re.sub(r'(,)', '', m)
        
        lst = m.split(' ')
        lst = lst[:-1] # tril off the last space in list
        lst.append(b)
        
      return lst
    
    
    numbers = []
    if self.dbTable == 'FloridaLotto':
      if match:
        m1 = match.group(1)
        m1 = re.sub(r'(-)', ' ', m1)
        m2 = match.group(3)
    elif self.dbTable == 'MegaMillions' or self.dbTable == 'PowerBall':
      numbers = [n1, n2, n3, n4, n5, n6]
      return numbers
    else:
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

    if self.dbTable == 'lottery' or self.dbTable == 'BC49':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/Lotto649.asp").read()

      match1 = re.search(r'(<b>Wednesday,\s*)(.+)', file)
      match2 = re.search(r'(<b>Saturday,\s*)(.+)', file)
      if match1:
        m1 = match1.group(2)
      if match2:
        m1 = match2.group(2)

    elif self.dbTable == 'lottomax':
      file = urlopen("http://www.bclc.com/app/DidYouWin/WinningNumbers/LOTTOMAX.asp").read()
      match = re.search(r'(<b>Friday,\s*)(.+)', file)
      if match:
        m1 = match.group(2)

    elif self.dbTable == 'FloridaLotto':
      file = urlopen("http://www.flalottery.com/lottoMain.do").read()
      match = re.search(r'(DRAW DATE:&nbsp;)(.+)(</td>)', file)
      if match:
        return match.group(2)

    elif self.dbTable == 'MegaMillions':
      file = urlopen("http://www.calottery.com/Games/MegaMillions/WinningNumbers/").read()
      match = re.search(r'(.+lblDrawDate\">)(\d+-\d+-\d+)(<.+)', file)
      if match:
        dat = match.group(2)
        dat = re.sub(r'(-)', '/', dat)
        return dat
    elif self.dbTable == 'PowerBall':
      file = urlopen("http://www.powerball.com/powerball/pb_numbers.asp").read()
      match = re.search(r'(<span class=\"link_white\"><b>)(\d*\/\d*\/\d*)(<.+)', file)
      if match:
        dat = match.group(2)
      return dat
    elif self.dbTable == 'NYLotto':
      file = urlopen("http://www.alllotto.com/latest_new_york_lottery_results.php").read()
      match = re.search(r'(<div class=\'normal\' id=\'lottodt\'>)(\d*-\d*-\d*)(<.+)', file)
      if match:
        dat = match.group(2)
      return dat
               

        
    
    m2 = re.search(r'(.+)(</b>.+)', m1)
    if m2:
      m = m2.group(1)

    return m


  def dbInsert(self):

    dn = self.dbLastDrawNumber()
    ddate = self.searchDrawDate()
    if self.dbTable == 'FloridaLotto' or self.dbTable == 'MegaMillions' or self.dbTable == 'PowerBall' or self.dbTable == 'NYLotto':
      ddate = self.searchDrawDate()
    else:
      ddate = self.convertDate()
    lastdate = self.dbLastDrawDate()

    if ddate == lastdate:
      print ddate
      print lastdate
      return

    dn += 1
    if self.dbTable == 'lottery' or self.dbTable == 'BC49' or self.dbTable == 'FloridaLotto' or self.dbTable == 'NYLotto':
      (n1, n2, n3, n4, n5, n6, b) = self.searchDrawNumbers()
      self.cursor.execute( "insert into " + self.dbTable + " VALUES(%d, '%s', %d, %d, %d, %d, %d, %d, %d)" %
                           (int(dn), ddate, int(n1), int(n2), int(n3), int(n4), int(n5), int(n6), int(b)))

    elif self.dbTable == 'lottomax':
      (n1, n2, n3, n4, n5, n6, n7, b) = self.searchDrawNumbers()
      self.cursor.execute("insert into " + self.dbTable + " VALUES(%d, '%s', %d, %d, %d, %d, %d, %d, %d, %d)" %
                          (int(dn), ddate, int(n1), int(n2), int(n3), int(n4), int(n5), int(n6), int(n7), int(b)))

    elif self.dbTable == 'MegaMillions' or self.dbTable == 'PowerBall':
      (n1, n2, n3, n4, n5, b) = self.searchDrawNumbers()
      self.cursor.execute("insert into " + self.dbTable + " VALUES(%d, '%s', %d, %d, %d, %d, %d, %d)" %
                          (int(dn), ddate, int(n1), int(n2), int(n3), int(n4), int(n5), int(b)))
      
    else:
      print 'Unknown database table'
      return

  def dbClose(self):
    self.dbConnection.close()

def main():
  if len(sys.argv) > 1:
    tblName = sys.argv[1]
  else:
    tblName = 'NYLotto'
  lottoDataInput = LottoDataInput(tblName)

  lottoDataInput.dbInsert()
  lottoDataInput.dbSelectAll()
  lottoDataInput.dbClose()
  

if __name__ == '__main__':
  main()

  
