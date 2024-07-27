import 'dart:convert';

import 'package:auth/widgets/cool_button.dart';
import 'package:auth/widgets/text_section.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class AddPage extends StatefulWidget{
  AddPage({super.key});
  @override
  State<StatefulWidget> createState() {
    return AddPageState();
  }
}

class AddPageState extends State<AddPage>{
  TextEditingController _nameTextController = TextEditingController();
  TextEditingController _priceTextController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Добавить")),
      body: SingleChildScrollView(child: Column(children: [
        TextSection(text: "Название", controller: _nameTextController, obscureText: false),
        TextSection(text: "Цена", controller: _priceTextController, obscureText: false),
        CoolButton(text: "Сохранить", function: addProduct)
      ],),)
    );
  }
  void addProduct() async {
    var url = Uri.http("localhost:5194", "/product");
    var headers = {"Content-type" : "application/json"};
    var content = {
      "name" : _nameTextController.text,
      "price" : _priceTextController.text
    };
    var body = jsonEncode(content);
    var response = await http.post(url, headers: headers, body: body);
    if (response.statusCode != 200) return;
    Navigator.pop(context);
  }
}