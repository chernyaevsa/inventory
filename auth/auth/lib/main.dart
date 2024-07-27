import 'package:auth/pages/add_page.dart';
import 'package:auth/pages/auth_page.dart';
import 'package:auth/pages/main_page.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "Inventory Auth",
      initialRoute: "/auth",
      routes: <String, WidgetBuilder>{
        "/auth" : (BuildContext context) => const AuthPage(),
        "/" : (BuildContext context) => MainPage(),
        "/add" : (BuildContext context) => AddPage(),
      },
    );
  }
}
