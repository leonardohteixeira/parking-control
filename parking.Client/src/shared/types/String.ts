export class String {

  static Empty = '';

  static isNullOrEmpty(str?: string): boolean {
    return str == undefined || str == this.Empty;
  }

  static isNotNullOrEmpty(str?: string): boolean {
    return str != undefined && str != this.Empty;
  }

  static fromArray(array: any[]): string {
    let string = this.Empty;

    array.forEach(item => {
      string = string + item + '\n';
    });

    return string;
  }

  static unpluralize(str?: string): string {
    if (this.isNullOrEmpty(str))
      return this.Empty;

    return str!.split(' ').map(word => word.endsWith('s') ? word.slice(0, -2) : word).join(' ');
  }

  static concatValidValues<T>(source: T, properties: Array<keyof T>, separator: string): string {
    let text = '';
    properties.forEach(prop => {
      let value = source[prop] as any;

      if (String.isNotNullOrEmpty(value))
        text = `${text}${text != '' ? separator : ''}${value}`
    });
    return text;
  }
}
