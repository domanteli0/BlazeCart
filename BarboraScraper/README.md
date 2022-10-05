#Barbora spider basic info

In oder to activate the spider follow these steps:

1) open cmd
  - on windows works only in cmd. for some reason
2) got to Python3VirtualEnvironment\Scripts
3) run activate.bat script
  - this activate the =virtual environment for scrapy to work
4) got to Python3VirtualEnvironment\BarboraScraper
  - there is another BarboraScraper inside. DON'T go inside
5) run scrapy crawl -O <!your_file_name!>.json
  - replace <!your_file_name!> with the desired file name
  - use -O (UPPERCASE O)if you want overwrite the file, -o (lowercase o) if you want to uppend the file 

DONE! Now after few(-teen) seconds there should be a file generated in the Python3VirtualEnvironment\BarboraScraper direcrory with the scraped data.

Known problems:
  - The terminal says that command scrapy could not be recognised
    -- POSSIBLE REASON: activate.bat file did not run properly
    -- POSSIBLE SOLUTION: run the same file in a different terminal
  - The terminal says that crawl could not be recognised
    -- POSSIBLE REASON: you tried running the spider from the wrong directory
    -- POSSIBLE SOLUTION: go to Python3VirtualEnvironment\BarboraScraper direcrory (NOT Python3VirtualEnvironment\BarboraScraper\BarboraScraper) and run the command again

For any other problems, refer to the terminal

TO DO:
  - data validation
    -- there is no data validation / connection validation appart from what scrapy does by default, so if the file is empty, check the ternminal, it could be that there is no connection to the internet, or any other problems

##Disclaimer:

**THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.