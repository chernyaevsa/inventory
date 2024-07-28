import 'dart:collection';
import 'dart:convert';

import 'package:auth/preferences/preferences.dart';
import 'package:auth/preferences/preferences_const.dart';
import 'package:auth/widgets/image_section.dart';
import 'package:auth/widgets/text_section.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import '../widgets/cool_button.dart';

class AuthPage extends StatefulWidget{
  const AuthPage({super.key});

  @override
  State<StatefulWidget> createState() {
    return AuthPageState();
  }
}

class AuthPageState extends State<AuthPage>{

  final _addressController = TextEditingController();
  final _passwordController = TextEditingController();
  var status = "";

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance
      .addPostFrameCallback((_) => loadAuth());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Авторизация"),automaticallyImplyLeading: false),
      body: SingleChildScrollView(
        child: Center(child: Container(
        constraints: const BoxConstraints(maxWidth: 1200),
        child: Column(
          children: [
            const ImageSection(image: "images/lake.jpg"),
            TextSection(text: "Адрес", controller: _addressController, obscureText: false,),
            TextSection(text: "Пароль", controller: _passwordController, obscureText: true,),
            CoolButton(function: auth, text: "Вход"),
            Text("$status")
            ],
        ))),
      )
    );
    
  }

  void auth() async{
    var url = Uri.http(_addressController.text, "/user/all");
    var request = HashMap<String, dynamic>();
    request["apiKey"] = _passwordController.text;
    var headers = HashMap<String, String>();
    headers["Content-type"] = "application/json";
    String json = jsonEncode(request);
    http.Response? response;
    try {
      response = await http.post(url, body: json, headers: headers);
    } catch (e) {
      status = "Connection error";
      setState(() {});
    }
    if (response == null) {return;}
    
    if (response.statusCode == 200) {
      await Preferences.saveAuth(_addressController.text, _passwordController.text);
      Navigator.pushNamed(context, "/");
    } else {
      setState(() {
        var responseMap = jsonDecode(response!.body);
        status = responseMap["message"];
      });
    }
    setState(() {});
  }

  void loadAuth() async{
    var address = await Preferences.getAddress();
    var password = await Preferences.getPassword();
    if (address == PreferencesConst.address || password == PreferencesConst.defaultPassword) return;
    _addressController.text = address;
    _passwordController.text = password;
    auth();
  } 

  
}