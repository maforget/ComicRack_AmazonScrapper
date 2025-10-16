import clr
import sys
import System
clr.AddReference('System.Windows.Forms')
from System.Windows.Forms import *

clr.AddReference('System.Drawing')
from System.Drawing import *

clr.AddReference('AmazonScrapper')
from AmazonScrapper.Dialog import frmConfig
from AmazonScrapper import Plugin

from cYo.Projects.ComicRack.Engine import *

#@Name	 Amazon Scrapper Config
#@Key    AmazonScrapper
#@Hook   ConfigScript
def Config():
	frm = frmConfig()
	ThemeMe(frm)
	frm.ShowDialog()

#@Name	Amazon Scrapper
#@Image amazon.png
#@Key	AmazonScrapper
#@Hook	Books, Editor
#@Description	Scrapes info from Amazon (Formally Comixology)
def Process(books):
	try:
		if ComicRack.App.ProductVersion >= '0.9.182':
			Plugin.Run(ComicRack.App, books, ComicRack.Theme)
		else:
			Plugin.Run(ComicRack.App, books)
	except Exception as e:
		print('Generic Exception: ', e)
		return

def ThemeMe(control):
    if ComicRack.App.ProductVersion >= '0.9.182':
            ComicRack.Theme.ApplyTheme(control)

