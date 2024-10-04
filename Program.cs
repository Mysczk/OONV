using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;


class Program{
    static void Main(string[] args){
        string path = "Data.csv";
        string path2 = "DataAlter.csv";

        Analytic jouda = new Analytic(CSVDatabase.GetInstance(path));
        Analytic nouma = new Analytic(CSVDatabase.GetInstance(path2));
    
        jouda.Write("Ahoj\n");
        Console.WriteLine(nouma.Read());
        nouma.Write("Zdar\n");
        Console.WriteLine(jouda.Read());
    }
}

class Analytic{

    public CSVDatabase db {get; set;}
    public Analytic(CSVDatabase db){
        this.db = db;
    }

    public void Write(string data){
        using (StreamWriter sw = File.AppendText(this.db.Path)){
            sw.Write(data);
        }
    }

    public string Read(){
        string text = "";
        using (StreamReader sr = File.OpenText(this.db.Path)){
            text = sr.ReadToEnd();
        }
        return text;
    }
}


class CSVDatabase{
    private static CSVDatabase? _db;
    public string Path {get; private set;}
    private CSVDatabase(string path){
        this.Path = path;
    }

    public static CSVDatabase GetInstance(string path){
        if (CSVDatabase._db == null){
            CSVDatabase._db = new CSVDatabase(path);
        }
        return CSVDatabase._db;
    }
}