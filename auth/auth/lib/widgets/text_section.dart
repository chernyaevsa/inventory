import 'package:flutter/material.dart';

class TextSection extends StatelessWidget{
  final String text;
  final TextEditingController controller;
  final bool obscureText;
  const TextSection({super.key, required this.text, required this.controller, required this.obscureText});
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(15),
      child: TextField(
        controller: controller,
        obscureText: obscureText, 
        decoration: InputDecoration(
          border: const OutlineInputBorder(), 
          labelText: text)
          )
      );
  }
}