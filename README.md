# Amazon Scrapper for ComicRack

#### This plugin uses the Amazon (formally Comixlogy) search to scrape data from Amzon pages and adds them to the metadata in ComicRack.

This isn't meant to be use as a replacement for the [ComicVine Scrapper](https://github.com/cbanack/comic-vine-scraper), which is miles ahead of this. First the data on Amazon isn't as detailed and this doesn't use any API, it just scrapes the pages. Its main use is primally to get some info for those releases that aren't yet available on ComicVine. Also, heavy use of this tool could probably mean that Amazon will block you or ask for if you aren't a robot, so use with moderation. To prevent blocking (I had it happen only once in developing this), the program will use a random user agent. So use it on a couple of books, not your library of thousands. 

The interface is reminiscent of the ComicVine Scrapper, so you should be able to use without too much efforts. The only difference between the former are as follow:

-	You can double-click on an entry to open the corresponding Amazon webpage in your browser.
-	It uses your Serie & Number for the search, so it should return a restricted list of releases. You can use the StrictSearch option for a even more restricted search.
-	By default, it will return the book you searched for (versus the ComicVine Scrapper that returns series). There is a Group by Series checkbox that you can enable that will group by series, if the information is available in the search results. 
-   When grouped by series, a second window asking you to select the specific issue will open. 
-   When grouped by series, double-clicking will open the series page instead of the book page. 
-	The number that it returns will be detected from the title instead of the order in its series page (unless no number that aren't years are found). The reason is that the position in the series page isn’t always the correct number.
-	There is no cleanup of the data it returns like with Publisher and Title. 
-	This will not try to choose the correct book automatically like ComicVine Scrapper. You will need to select the correct book every time. The data on Amazon isn’t well organized enough like the former Comixology.

Normally ComicRack plugins uses IronPython (a mixture of Python & .NET Framework). This is now obsolete and very hard to develop & debug for, so this plugin is completely done in the .NET Framework. The only python code is to call the .NET Code (1 line). So technically you could use the included executable directly, it will work on its own, but you will only be able to search and not add any metadata to any books.




----

##### Also for ComicRack:

- ###### [Data Manager](https://github.com/maforget/CRDataManager) let's you manipulate your data for ComicRack. Fix the various bugs in the latest v2 release.
- ###### [Keygen for ComicRack](https://github.com/maforget/ComicRackKeygen). A keygen to activate because of the now dead website. Also includes a RAR5 Support Pack to enable RAR5 functionality.
- ###### [Bédéthèque Scrapper v2](https://github.com/maforget/Bedetheque-Scrapper-2) to scrap data from the French BD site Bédétheque.
- ###### [FindImageResolution.py](https://gist.github.com/maforget/63558612d19410c9807d6e87a494cf4a) to determine the resolution of a comic. Click raw and save as the file, copy the file in either `%programfiles%\ComicRack\Scripts` or `%appdata%\cYo\ComicRack\Scripts`. Use it by right-clicking--> Automate-->Find Image Resolution. It will add the resolution to Scan Information in the Plot & Notes tab.
- ###### [fullscreen.py](https://gist.githubusercontent.com/maforget/186a99205140acd3f7d3328ad1466e62/raw/8c7c0ecab28fb9a6037adbe19ff553e3597cccd6/fullscreen.py). It will automatically fullscreen the application when either opening a book or starting the application depending on which you enable). Copy the file in either `%programfiles%\ComicRack\Scripts` or `%appdata%\cYo\ComicRack\Scripts`.
- ###### [comicrack-copy-move-field](https://github.com/maforget/comicrack-copy-move-field). Moves or copies info from one field to another. Can either replace or append to the destination field. Small update from the original to permit dates to be copied or moved.