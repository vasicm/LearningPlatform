import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WordComponent } from './word/word.component';
import { WordEditComponent } from './word-edit/word-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [WordComponent, WordEditComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    WordComponent,
    WordEditComponent
  ]
})
export class ComponentsModule { }
