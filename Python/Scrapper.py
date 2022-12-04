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
	frm.ShowDialog()

#@Name	Amazon Scrapper
#@Image amazon.png
#@Key	AmazonScrapper
#@Hook	Books, Editor
#@Description	Scrapes info from Amazon (Formally Comixology)
def Process(books):
	try:
		Plugin.Run(ComicRack.App, books)
	except Exception as e:
		print('Generic Exception: ', e)
		return