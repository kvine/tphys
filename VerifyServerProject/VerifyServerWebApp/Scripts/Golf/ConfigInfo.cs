using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ConfigInfo
{
    public static Config config = new Config();
    public class Config 
    {
       public bool UseNewPhysics 
       { 
           get { return true; } 
       }
    }
}


