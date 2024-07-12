export class String {

  static Empty = '';

  static isNullOrEmpty(str?: string): boolean {
    return str == undefined || str == this.Empty;
  }

  static isNotNullOrEmpty(str?: string): boolean {
    return str != undefined && str != this.Empty;
  }
}
