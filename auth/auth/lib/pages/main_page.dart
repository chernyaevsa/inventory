import 'dart:convert';

import 'package:auth/widgets/cool_button.dart';
import 'package:auth/widgets/image_section.dart';
import 'package:auth/widgets/text_section.dart';
import 'package:flutter/material.dart';

import 'package:http/http.dart' as http;


class MainPage extends StatefulWidget{
  MainPage({super.key});
  @override
  State<StatefulWidget> createState() {
    return MainPageState();
  }
}

class MainPageState extends State<MainPage>{
  var listProducts = [];
  MainPageState(){
    //getProducts();
  }
  var selectedIndex = 0;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Shop"),
      automaticallyImplyLeading: false),
      body: 
      SingleChildScrollView(child: 
        Column(
          crossAxisAlignment: CrossAxisAlignment.end,
          children: [
          Row(
            crossAxisAlignment: CrossAxisAlignment.end,
            mainAxisSize: MainAxisSize.min,
            children: [
            CoolButton(text: "🔄", function: getProducts),
            SizedBox(width: 10,),
            CoolButton(text: "➕", function: addProduct),
            SizedBox(width: 10,),
            CoolButton(text: "➖", function: deleteProduct),
            SizedBox(width: 10,),
          ],),
          
          ListView.builder(
              scrollDirection: Axis.vertical,
              shrinkWrap: true,
              padding: EdgeInsets.all(8),
              itemCount: listProducts.length,
              itemBuilder: (context, index){
                //return Text(listProducts[index]["name"]);
                var price = listProducts[index]["price"];
                return ListTile(
                  tileColor: selectedIndex == index ? Colors.blue : null,
                  onTap: (){
                    setState(() {
                      selectedIndex = index;
                    });
                    
                  },
                  title: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  mainAxisSize: MainAxisSize.min,
                  children: [
                  Text(listProducts[index]["name"], style: TextStyle(fontSize: 24),),
                  Text("$price"),
                ],),);
                
            })
        ],)
      ,)
            
        );
  }

  void getProducts() async{
    var url = Uri.http("localhost:5194", "/product/all");
    var response = await http.get(url);
    if (response.statusCode != 200) return;
    listProducts = jsonDecode(response.body);
    setState(() {
      
    });
  }
  void deleteProduct() async {
    var id = listProducts[selectedIndex]["id"];
    var url = Uri.http("localhost:5194", "/product/$id");
    var response = await http.delete(url);
    if (response.statusCode != 200) return;
    getProducts();
  }
  void addProduct() async{
    await Navigator.pushNamed(context, "/add");
    getProducts();
  }
}