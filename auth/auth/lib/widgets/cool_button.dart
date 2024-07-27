import 'package:flutter/material.dart';

class CoolButton extends StatelessWidget{
  final String text;
  final Function()? function;
  const CoolButton({super.key, required this.text, required this.function});
  @override
  Widget build(BuildContext context) {
    return OutlinedButton(onPressed: function, child: Text(text, style: TextStyle(fontSize: 16),));
  }
}