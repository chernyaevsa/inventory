import 'package:auth/preferences/preferences_const.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Preferences {
  static late SharedPreferences _sharedPreferences;

  static updatePreferences() async{
    _sharedPreferences = await SharedPreferences.getInstance();
  }

  static saveAuth(String address, String password) async {
    await setAddress(address);
    await setPassword(password);
  }

  static setAddress(String address) async {
    await updatePreferences();
    await _sharedPreferences.setString(PreferencesConst.address, address);
  }
  static Future<String> getAddress() async{
    await updatePreferences();
    var address = _sharedPreferences.getString(PreferencesConst.address);
    address ??= PreferencesConst.defaultAddress;
    return address;
  }
  static setPassword(String password) async{
    await updatePreferences();
    await _sharedPreferences.setString(PreferencesConst.password, password);
  }
  static Future<String> getPassword() async{
    await updatePreferences();
    var password = _sharedPreferences.getString(PreferencesConst.password);
    password ??= PreferencesConst.defaultPassword;
    return password;
  }
}