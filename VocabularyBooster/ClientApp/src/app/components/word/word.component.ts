import { Component, Input, OnInit } from '@angular/core';
import { Word } from 'src/app/data/api/models';

@Component({
  selector: 'vb-word',
  templateUrl: './word.component.html',
  styleUrls: ['./word.component.css']
})
export class WordComponent implements OnInit {
  @Input('word') word: Word
  
  constructor() { }

  ngOnInit() {
  }

}
