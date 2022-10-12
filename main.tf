terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.16"
    }
  }

  required_version = ">= 1.2.0"

  backend "s3"{
    region = "us-west-2"
    bucket = "tf-gabay"
    key    = "tfgabay.tfstate"
  }
}

provider "aws" {
  region  = "us-west-2"
}

resource "aws_instance" "app_server" {
  ami           = "ami-830c94e3"
  instance_type = "t2.micro"

  tags = {
    Name = "ExampleAppServerInstance"
  }
}

resource "aws_s3_bucket" "barak-tf" {
  bucket = "mye-tf-bucket"

  tags = {
    Name        = "My bucket"
    Environment = "Dev"
  }
}

resource "aws_s3_bucket_acl" "barak-tf-acl" {
  bucket = aws_s3_bucket.barak-tf.id
  acl    = "private"
}
