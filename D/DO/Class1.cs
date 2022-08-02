using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DO;


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
    public string @default { get; set; }
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

public class Trail
{
    public double lat { get; set; }
    public double lng { get; set; }
    public int alt { get; set; }
    public int spd { get; set; }
    public int ts { get; set; }
    public int hd { get; set; }
}

