using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


public class Code
{
    public string iata { get; set; }
    public string icao { get; set; }
}

public class Generic
{
    public Status status { get; set; }
    public EventTime eventTime { get; set; }
}

public class Images
{
    public List<Thumbnail> thumbnails { get; set; }
    public List<Medium> medium { get; set; }
    public List<Large> large { get; set; }
}

public class Large
{
    public string src { get; set; }
    public string link { get; set; }
    public string copyright { get; set; }
    public string source { get; set; }
}

public class Medium
{
    public string src { get; set; }
    public string link { get; set; }
    public string copyright { get; set; }
    public string source { get; set; }
}

public class Model
{
    public string code { get; set; }
    public string text { get; set; }
}

public class Number
{
    public string Default { get; set; }
    public object alternative { get; set; }
}

public class Other
{
    public int eta { get; set; }
    public int updated { get; set; }
}

public class Thumbnail
{
    public string src { get; set; }
    public string link { get; set; }
    public string copyright { get; set; }
    public string source { get; set; }
}

