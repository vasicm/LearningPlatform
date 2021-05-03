import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalCloseService } from 'src/app/common/modal-close.service';
import { Word } from 'src/app/data/api/models';
import { WordService } from 'src/app/data/api/services';

@Component({
  selector: 'vb-word-edit-modal',
  templateUrl: './word-edit-modal.component.html',
  styleUrls: ['./word-edit-modal.component.css']
})
export class WordEditModalComponent implements OnInit {
  data: {
    word: Word;
  };
  form: FormGroup;

  constructor(
    private wordService: WordService,
    private modalClose: ModalCloseService
  ) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      expression: new FormControl("", Validators.minLength(1)),
      definition: new FormControl("", Validators.minLength(1)),
      example: new FormControl("", Validators.minLength(1)),
      grammaticalCategories: new FormControl("", Validators.minLength(1)),
      thesaurus: new FormControl(""),
      callocations: new FormControl(""),
      cefr: new FormControl(""),
      topic: new FormControl(""),
    });
  }

  submit() {
    this.wordService.AddWord({
      body: {
        expression: this.form.getRawValue().expression.trim(),
        sense: [
          {
            definition: this.form.getRawValue().definition.trim(),
            example: this.form.getRawValue().example.trim(),
            grammaticalCategories: this.form.getRawValue().grammaticalCategories.trim(),
            thesaurus: this.form.getRawValue().thesaurus.trim(),
            callocations: this.form.getRawValue().callocations.trim(),
            cefr: this.form.getRawValue().cefr.trim(),
            topic: this.form.getRawValue().topic.trim(),
          }
        ]
      }
    }).subscribe();
  }

  close() {
    this.modalClose.hide();
  }
}
