import urllib.request  
from pprint import pprint 

# parsing service
from html_table_parser import HTMLTableParser
  
# converting into pandas dataframe
import pandas as pd 
  
def url_get_contents(url): 

    req = urllib.request.Request(url=url) 
    f = urllib.request.urlopen(req) 
  
    #reading contents of the website 
    return f.read() 

xhtml = url_get_contents('https://www.google.com/recaptcha/api2/demo').decode('utf-8') 

p = HTMLTableParser() 
print(xhtml)
  

p.feed(xhtml) 
pprint(p.tables[0]) 

print("\n\nPANDAS DATAFRAME\n") 
print(pd.DataFrame(p.tables[0]))