import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Word } from 'src/app/data/api/models';
import { WordService } from 'src/app/data/api/services';

@Component({
  templateUrl: './dictionary-page.component.html',
  styleUrls: ['./dictionary-page.component.css']
})
export class DictionaryPageComponent implements OnInit {


  form: FormGroup;
  wordList: Word[];

  constructor(
    private wordService: WordService
  ) { }

  ngOnInit() {
    this.form = new FormGroup({
      query: new FormControl("", Validators.minLength(1))
    });
  }

  submit() {
    this.wordService.SearchWord({
      phrase: this.form.getRawValue().query.trim()
    }).subscribe(x => {
      this.wordList = x;
    })
  }
}
