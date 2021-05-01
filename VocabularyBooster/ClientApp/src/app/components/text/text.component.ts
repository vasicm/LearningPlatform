import { Component, Input, OnInit } from '@angular/core';
import { Text } from 'src/app/data/api/models';

@Component({
  selector: 'vb-text',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.css']
})
export class TextComponent implements OnInit {
  @Input('text') text: Text
  constructor() { }

  ngOnInit() {
  }

}
