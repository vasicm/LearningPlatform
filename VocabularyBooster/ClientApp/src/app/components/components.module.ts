import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WordComponent } from './word/word.component';

@NgModule({
  declarations: [WordComponent],
  imports: [
    CommonModule
  ],
  exports: [
    WordComponent
  ]
})
export class ComponentsModule { }
