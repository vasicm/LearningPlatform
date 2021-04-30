import { Component } from '@angular/core';
import { TextService } from '../data/api/services';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  constructor(private textService: TextService) {
  }

  public incrementCounter() {
    this.textService.SearchText({
      apiVersion:"1",
      phrase: "full of risks and"
    }).subscribe(x => {
      console.log(x);
      console.log(x[0].content);
    });

    this.currentCount++;
  }
}
