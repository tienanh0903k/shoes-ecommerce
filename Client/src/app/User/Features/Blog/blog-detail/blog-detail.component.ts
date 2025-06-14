import { Component, OnInit } from '@angular/core';
import { Blog } from '../models/blog.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { BlogService } from '../services/blog.service';
import { IMG_URL } from '../../../../app.config';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-detail',
  imports: [CommonModule],
  templateUrl: './blog-detail.component.html',
  styleUrl: './blog-detail.component.css',
})
export class BlogDetailComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  id: string | null = null;
  blog?: Blog;
  paramsSubcription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private blogService: BlogService
  ) {}

  ngOnInit(): void {
    this.paramsSubcription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
      },
    });

    if (this.id) {
      this.blogService.getBlogDetail(this.id).subscribe({
        next: (response) => {
          this.blog = response;
        },
      });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubcription?.unsubscribe();
  }
}
