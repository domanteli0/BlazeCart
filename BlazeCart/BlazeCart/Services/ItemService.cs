﻿using BlazeCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazeCart.Services;

public class ItemService
{
    //Retrives a whole item list
    List<Item> itemList = new();

    public async Task<List<Item>> GetItems()
    {

        //If list already exists:
        if (itemList.Count > 0) {
            return itemList;
        }
        
        //Else get list
        using var stream = await FileSystem.OpenAppPackageFileAsync("shopItems.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        itemList = JsonSerializer.Deserialize<List<Item>>(contents);
        return itemList;
        
  
    }

        /*DATA GETTING FROM REMOTE SOURCE:
        HttpClient httpClient;
        string source_link = null;

        public ItemService()
        { 
            this.httpClient = new HttpClient();
        }

        public async Task<List<Item>> GetItems(){
            if (itemList.Count > 0)
                return itemList;

            var response = await httpClient.GetAsync(source_link);

            if (response.IsSuccessStatusCode)
            {
                itemList = await response.Content.ReadFromJsonAsync<List<Item>>();
            }
    
        }
        */

}