# spec/spec_helper.rb
begin
  require 'rspec'

  RSpec.configure do |config|
    config.color = true
    # Human-friendly output only when run in a real terminal
    config.default_formatter = 'doc' if $stdout.tty?
  end
rescue LoadError
  # RSpec not available in debug mode, skip configuration
end