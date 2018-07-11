using System.Collections.Generic;
using System;
using ToDoList;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;

    public Item (string Description, int Id = 0)
    {
      _description = Description;
      _id = Id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Item> GetAll()
    {
        List<Item> allItems = new List<Item> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"Select * FROM items;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int itemId = rdr.GetInt32(0);
            string itemDescription = rdr.GetString(1);
            Item newItem = new Item(itemDescription, itemId);
            allItems.Add(newItem);
        }
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
        return allItems;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn !=null)
      {
        conn.Dispose();
      }
    }
    // public void Save()
    // {
    //   _instances.Add(this);
    // }
    // public static void ClearAll()
    // {
    //   _description.Clear();
    // }
    // public static Item Find(int searchId)
    // {
    //   return _description[searchId-1];
    // }

  }
}
