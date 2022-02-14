using System;
using System.Collections.Generic;

public interface ITemplateLoader
{
    List<Tuple<string,string>>  LoadSingleTemplateFile();
    List<Tuple<string, string>> LoadGroupTemplateFiles();
    Tuple<string, string>       LoadSavedEntityFile(string saveFileName);
}