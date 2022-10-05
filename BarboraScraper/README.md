# Barbora scraper how-to-use info

In oder to activate the spider follow these steps:

## Windows

1) open cmd
  - on windows works only in cmd. for some reason
  - don't have linux, don't know what (if anything) works

2) navigaate to Python3VirtualEnvironment\Scripts

3) run activate.bat script
  - this activate a python virtual environment for scrapy to work
  - after activation, the terminal prompt should be updated with text "(Python3VirtualEnvironment)". If it doesn't show, likely that the virtual environment wasn't activated and the spider won't work 
  - it deactivate either by running the deactivate.bat script or closing the terminal

4) navigate to Python3VirtualEnvironment\BarboraScraper
  - there is another BarboraScraper inside. DON'T go inside

5) run scrapy --version
  - should output "Scrapy 2.6.3" or something simmilar to the terminal
  - if scrapy was not recognised as command - the activate.bat script didn't activate the virtual environment. try a different terminal

6) run scrapy crawl -O <!your_file_name!>.json
  - replace <!your_file_name!> with the desired file name
  - use -O (UPPERCASE O)if you want overwrite the file, -o (lowercase o) if you want to uppend the file 

DONE! Now after few(-teen) seconds there should be a file generated in the Python3VirtualEnvironment\BarboraScraper direcrory with the scraped data.

The actual spider code is here: Python3VirtualEnvironment\BarboraScraper\BarboraScraper\spiders\maxima_spider.py

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
  - check Linux / MacOS activation steps
##Disclaimer:

**THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.