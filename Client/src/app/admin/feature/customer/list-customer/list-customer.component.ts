import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-list-customer',
  imports: [CommonModule],
  templateUrl: './list-customer.component.html',
  styleUrl: './list-customer.component.css'
})
export class ListCustomerComponent {

  customers$?: Observable<Customer[]>;
  model?: Customer;
  constructor(private customerService: CustomerService){
    this.model ={
      id:'',
      email:'',
      fullName:'',
      address:'',
      status: true
    }
  }

  ngOnInit(): void{
    this.customers$ = this.customerService.getAllCustomer();
  }
  onClick(event : Event, id: string):void{
    
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.customerService.getCustomerById(id).subscribe(
      {
        next: (response: Customer) =>{
          this.model = {...response};
          this.model.address = "text Required";
          if (selectedValue === 'Block') {
            if(this.model != undefined){
              this.customerService.updateStatus(id, this.model).subscribe(
                {
                  next: response =>{
                    console.log("Update success");
                  },
                  error: err =>{
                    console.log(err.error);
                    
                  }
                }
              )
            }
          } else if (selectedValue === 'Active') {
            if(this.model != undefined){
              this.customerService.updateStatus(id, this.model).subscribe(
                {
                  next: response =>{
                    console.log("Update success");
                  },
                  error: err =>{
                    console.log(err.error);
                    
                  }
                }
              )
            }
          }
        },
        error: err =>{
          console.log(err);
        }
      }
    );
    
    
    
  }


}
